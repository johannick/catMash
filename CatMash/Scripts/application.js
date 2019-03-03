var app = new Vue({
    el: '#cats',
    data : {
        catOne : "http://25.media.tumblr.com/tumblr_m4pvakprVF1r6jd7fo1_500.jpg",
        catTwo : "http://25.media.tumblr.com/tumblr_m2g2ksSLML1qgkc80o1_400.gif",
        catOneScore : -100,
        catTwoScore: -100,
        cats: []
        },

    methods: {
        setImages : function() {
            
            var request = new XMLHttpRequest();
            request.open('GET', '/api/values', false);  // `false` makes the request synchronous
            request.send(null);

            this.cats = JSON.parse(request.responseText);
        },

        nextCatsToVote: function () {
            this.index = this.index || 0;
            if (this.index <= 1)
                this.setImages();
            this.catOne = this.cats[this.index].url;
            this.catTwo = this.cats[(this.index + 1) % this.cats.length].url;
            this.catOneScore = this.cats[this.index].rank / (1 + this.cats[this.index].votes);
            this.catTwoScore = this.cats[(this.index + 1) % this.cats.length].rank / (1 + this.cats[(this.index + 1) % this.cats.length].votes);            
        },


        voteLeft: function () {
            this.upVote(this.cats[this.index], this.cats[(this.index + 1) % this.cats.length]);
        },

        voteRight: function () {
            this.upVote(this.cats[(this.index + 1) % this.cats.length], this.cats[this.index]);
        },

        upVote: function (first, second) {

            $.post('/api/vote', JSON.stringify({ VoteFor: first.id, VoteAgainst: second.id }));
            this.index = (this.index + 2) % this.cats.length;
            this.nextCatsToVote();
        }
    }
});