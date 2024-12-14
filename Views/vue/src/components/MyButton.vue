<template>
     <button class="buttonMy">
        <slot></slot>
    </button>
</template>
<script>


export default {
    name:'myButton',
    
}
</script>

<style>
.buttonMy{
    background: white;
    padding:  5px 15px;
    border-radius: 15px;
}
</style>