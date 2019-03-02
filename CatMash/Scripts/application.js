var app = new Vue({
    el: '#cats',
    data : {
        catOne : "http://25.media.tumblr.com/tumblr_m4pvakprVF1r6jd7fo1_500.jpg",
        catTwo : "http://25.media.tumblr.com/tumblr_m2g2ksSLML1qgkc80o1_400.gif",
        catOneScore : -100,
        catTwoScore: -100,
        images : null,
        },

    methods: {
        setImages : function() {
            if (this.images != null)
                return;

            var request = new XMLHttpRequest();
            request.open('GET', '/api/values', false);  // `false` makes the request synchronous
            request.send(null);

            this.cats = JSON.parse(request.responseText);
            this.catOne = this.cats[0].url;
            this.catTwo = this.cats[1].url;
            this.catOneScore = this.cats[0].rank / ( 1 + this.cats[0].votes);
            this.catTwoScore = this.cats[1].rank / ( 1 + this.cats[1].votes);
        },
        voteLeft : function(){

            console.log("Left");
        },

        voteRight : function(){

            console.log("Right");
        },

        vote : function(){

        }
    }
});