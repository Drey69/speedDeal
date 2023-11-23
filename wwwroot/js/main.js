

var mainProg = {
    data(){
        return{
            countItems:0,
            items:[],
        }
    },
    computed: {
    },
    components:0,
    methods: {
        buttonAddClick(el){

            this.countItems++;
            let but = $("#add");
            let htm = `<div id="app-${this.countItems}"><div v-bind:style="itemStyle" v-on:mousedown="divmousedown" v-on:mouseup="divmouseup" v-on:mousemove="divmousemove"></div></div>`;
            but.after(htm);

            let modal = Vue.createApp(ModalWindow).mount(`#mod`);
            modal.show = true

            let vm = Vue.createApp(objprog).mount(`#app-${this.countItems}`);
            this.items.push(vm);

        }
    }

}

