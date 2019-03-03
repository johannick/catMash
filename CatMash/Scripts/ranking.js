var ranking = new Vue({
    el: '#catsRanking',
    data: {
        cats: []
    },

    methods: {
        nextCats: function () {
            var request = new XMLHttpRequest();
            request.open('GET', '/api/vote', false);  // `false` makes the request synchronous
            request.send(null);

            this.cats = JSON.parse(request.responseText);
        },
    }
});