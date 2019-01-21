<template>
    <h1>manage</h1>
</template>
<script>
export default {
    data(){
        return{
            name:"Holden"
        }
    },
    beforeRouteEnter:(to,from,next)=>{
        //beforeRouteEnter在data之前执行，
        //所以需要在next中回调调用name
        next(vm=>{
            alert("Hello" + vm.name);
        })
    }
}
</script>
