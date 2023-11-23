
    
<template>
  <div class="footer">
    <slot></slot>
  </div>
  <myDialog></myDialog>
</template>

<script>


    export default {
        name: 'HeaderView',
        data(){
            return{

            }
        },
        components: {
                
        }
    }
</script>


<style scoped>
    .footer{
        height: 50px;        
        padding: 10px;
        background-color: rgb(239, 174, 100);
        display: flex;
        flex-direction: row;
        border-radius: 5px;
    }
   
</style>