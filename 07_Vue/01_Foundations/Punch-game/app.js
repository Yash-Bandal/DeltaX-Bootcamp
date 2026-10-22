new Vue({
    el: '#vue-app',
    data:{
        health : 100,
        isEnded : false,
    },

    methods:{
        punch: function()
        {
            this.health -= 10;

            if (this.health <= 0) {
                this.isEnded = true;
            }
        },

        restart: function()
        {
            this.health = 100;
            this.isEnded = false;
        }
    },

});