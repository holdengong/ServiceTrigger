<template>
  <div id="app">
    <div class="container">
      <app-header></app-header>
    </div>
    <div class="container">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Header from './components/Header'
export default{
  components:{
    appHeader:Header
  },
  created(){
    fetch("/apis/test/testToken.php",{
      method:"post"
      ,body:"hello"
    })
    .then(result=>{
      console.log(result);
    });

    /* this.$axios.post("/apis/test/testToken.php",
    {username:"111",password:"222"}
    ).then(data=>{
        console.log(data);
    }) */
  }
}
</script>

<style>

</style>
