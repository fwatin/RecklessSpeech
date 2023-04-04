<script>
import axios from "axios";
export default {
  setup() {},
  data() {
    return {
      filePickerDialog: false,
      words: [],
      checkedWords: [],
    };
  },
  mounted() {
    axios
      .get("https://localhost:47973/api/v1/sequences")
      .then((response) => {
        this.words = response.data;
        console.log(
          this.words.length + " words set into the variable 'words'."
        );
      })
      .catch((error) => {
        console.error(error);
      });
  },
  methods: {
    async enrichInDutch() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      let count = 0;
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:47973/api/v1/sequences/Dictionary/dutch?id=${id}`
        );
        count++;
      }
      console.log(
        count + " séquences ont été enrichies avec succès en néérlandais."
      );
    },
    async enrichInEnglish() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      let count = 0;
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:47973/api/v1/sequences/Dictionary/english?id=${id}`
        );
        count++;
      }

      console.log(
        count + " séquences ont été enrichies avec succès en anglais."
      );
    },
    openFilePicker() {
      this.filePickerDialog = true;
    },
  },
};
</script>
<template>
  <div>
    <div>
      <button class="clickable" @click="openFilePicker">
        Import items.csv
      </button>
      <button class="clickable" @click="enrichInEnglish()">
        Enrichir en anglais
      </button>
    </div>

    <div>
      <table class="table">
        <tbody>
          <tr v-for="(file, index) in words" :key="file.name">
            <td>
              <input type="checkbox" v-model="checkedWords[index]" />
              <span>{{ file.word }}</span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>


<style scoped>
.clickable {
  cursor: pointer;
}
</style>