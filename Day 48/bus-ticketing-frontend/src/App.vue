<template>
  <div id="app">
    <AuthForm/>
  </div>
</template>

<script>

import AuthForm from './components/AuthForm.vue';

export default {
  name: 'App',
  components: {
    AuthForm, 
  },
};
</script>

<style>

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: Arial, sans-serif;
  background-color: #f4f4f4;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}
</style>
