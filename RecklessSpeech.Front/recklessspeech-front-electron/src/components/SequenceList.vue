<script>
export default {
  setup() {},
  data() {
    return {
      filePickerDialog: false,
    };
  },
  methods: {
    openFilePicker() {
      this.filePickerDialog = true;
    },
  },
};
</script>
<template>
  <div>
    <b-button class="clickable" @click="openFilePicker"
      >Import items.csv</b-button
    >
  </div>
</template>


<style scoped>
.clickable {
  cursor: pointer;
}
</style>