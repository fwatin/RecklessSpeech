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
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:47973/api/v1/sequences/Dictionary/english?id=${id}`
        );
      }

      console.log(
        selectedWords.length +
          " séquences ont été enrichies avec succès en anglais."
      );
    },
    async sendToAnki() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:47973/api/v1/sequences/send-to-anki?id=${id}`
        );
      }

      this.toast.info(
        selectedWords.length +
          " séquences ont été envoyées vers Anki avec succès."
      );
    },
    openFilePicker() {
      this.filePickerDialog = true;
    },
    onFileSelected(event) {
      this.selectedFile = event.target.files[0];
    },
    selectAll() {
      if (this.checkedWords.some((isChecked) => isChecked)) {
        // Au moins un élément est sélectionné, donc on les désélectionne tous
        this.checkedWords = [];
      } else {
        // Aucun élément n'est sélectionné, donc on les sélectionne tous
        this.checkedWords = this.words.map(() => true);
      }
    },
  },
};
</script>
<template>
  <div>
    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Sélectionner un fichier CSV</legend>
        <p>
          <input
            type="file"
            ref="fileInput"
            accept=".csv"
            @change="onFileSelected"
          />
          <button color="primary" @click="importCSV">Importer</button>
        </p>
      </fieldset>
    </div>

    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Enrichir</legend>
        <button class="clickable button-margin" @click="openFilePicker">
          Import items.csv
        </button>
        <button class="clickable button-margin" @click="enrichInEnglish()">
          Enrichir en anglais
        </button>
        <button class="clickable button-margin" @click="enrichInDutch()">
          Enrichir en néérlandais
        </button>
        <button class="clickable button-margin" @click="selectAll()">
          Selectionner tout
        </button>
      </fieldset>
    </div>
    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Envoyer</legend>
        <button class="clickable button-margin" @click="sendToAnki()">
          Envoyer vers Anki
        </button>
      </fieldset>
    </div>

    <div>
      <table class="table">
        <tbody>
          <tr v-for="(file, index) in words" :key="file.name">
            <td>
              <input type="checkbox" class="checkboxes" v-model="checkedWords[index]" />
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
.button-margin {
  margin: 5px;
}
.fieldset-container{
    margin: 5px;
}
.checkboxes{
    margin: 5px;
}
</style>