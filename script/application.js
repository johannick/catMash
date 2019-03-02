var app = new Vue({
	el: '#cats',
	data : {
		catOne : "http://25.media.tumblr.com/tumblr_m4pvakprVF1r6jd7fo1_500.jpg",
		catTwo : "http://25.media.tumblr.com/tumblr_m2g2ksSLML1qgkc80o1_400.gif",
		catOneScore : -100,
		catTwoScore : -100
	},

	methods : {
		voteLeft : function(){

			console.log("Left");
		},

		voteRight : function(){

			console.log("Right");
		}
	}
});