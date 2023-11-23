<template>
  <div class="screen">
    <HeaderView v-on:click="showDialog()">
      <svg v-on:click="showDialog" xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="btnTop bi bi-plus-circle" viewBox="0 0 16 16">
        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
        <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
      </svg>
    </HeaderView>
    <HomeView>
    <myDialog v-model:show="dialogVisibility" @create-link="createLink">    
    </myDialog> 
    </HomeView>
  </div>
</template>

<script>
import myDialog from '@/components/my-dialog.vue';
import HeaderView from '@/views/Header.vue';
import HomeView from '@/views/HomeView.vue';

export default {
      data(){
        return{
          dialogVisibility: false,
        }
      },

      methods:{
          createLink(event)
          {
            console.log(event);
          },
          showDialog()
          {
            this.dialogVisibility = !this.dialogVisibility;
          }
      },
      computed:
    {

    },

      components: {
        HeaderView,
        HomeView,
        myDialog
      }
}
</script>
<style>
body{


}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  width: 100%;
  height: 100%;

}

.screen
{
  height: 100%;
  width: 100%;
  background-color: rgb(245, 149, 65);
}
.btnTop {
  margin-top: 4px;
  padding: 1px;
  width: 40px;
  height: 40px;
  fill: dimgrey;
}
.btnTop:hover {
  padding: 1px;
  width: 45px;
  height: 45px;
  fill: #99f58c;
}
.btnTop:active {
  padding: 0;
  width: 45px;
  height: 45px;
  fill: black;
}
</style>
