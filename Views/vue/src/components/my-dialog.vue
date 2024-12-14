<template>
    <div class="dialog" v-if="show === true" @click="cencelClick">
        <div @click.stop class="dialog_content">
            <div class="dialogHeader">Добавление ссылки</div>
                <div class="dialogBody">
                    <div class="inputContener">
                        <input clas="dialogInput" type="text" v-model="link.description" placeholder="Описание"/>
                    </div>
                    <div class="inputContener">
                        <input clas="dialogInput" type="text" v-model="link.linkstr" placeholder="Адрес ссылки"/>
                    </div>
                    <div class="inputContener flex">
                        <button class="buttonMy" v-on:click="okClick()"> Ok </button> 
                        <button class="buttonMy" v-on:click="cencelClick()"> Cancel </button>
                    </div>
                    <div class="inputContener"></div>
            </div>
        </div>
    </div>
</template>

<script>


    export default {
        name:'myDialog',
        data(){
            return{
                link:{
                    description:'',
                    linkstr:'',
                },
            }
        },
        props:{
            show:{
                Type:Boolean                
            }
        },
        components:{

        },
        methods:{
            cencelClick(){
                this.link.description = '';
                this.link.linkstr = '';
                this.$emit('update:show', false);
            },
            okClick(){   
                this.$emit('update:show', false);         
                this.$emit('createLink', this.link);

            }
        }
    }   
</script>

<style scoped>
.buttonMy{
    background: #ebca9f;;
    padding:  5px 15px;
    border-radius: 15px;
}
.dialog{
    top:0;
    bottom: 0;
    right: 0;
    left: 0;
    background: rgba(0, 0, 0, 0.5);
    position: fixed;
    display: flex;
}

.dialog_content{
    margin: auto;
    background: #efdfc9;;
    border-radius: 15px;
    min-height: 80px;
    min-width: 120px;
}
.dialogBody{
    padding: 0px 10px 0 10px;
}
.inputContener{
    padding: 5px 0 0 0 ;
}
.dialogHeader{
    padding: 3px 8px ;
    background-color: #f2d8a9;
    border-radius: 15px 15px 0 0;
}
.flex{
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}
</style>