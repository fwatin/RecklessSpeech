<script>
import axios from "axios";
const backendPort = process.env.NODE_ENV === "development" ? "5000" : "5000";
const http = process.env.NODE_ENV === "development" ? "http" : "http";
const baseUrl = `${http}://localhost:${backendPort}`;
export default {
  data() {
    return {
      filePickerDialog: false,
      jsonPickerDialog: false,
      sequences: [],
      checkedSequenceIndexes: [],
      selectedJson: null,
      enrichProgression: 0,
      isSending: false,
      lastSelectedSequenceIndex: null,
    };
  },
  created() {
    this.import();
  },
  methods: {
    selectSequence(index, isShiftPressed) {
      if (isShiftPressed && this.lastSelectedSequenceIndex !== null) {
        const start = Math.min(this.lastSelectedSequenceIndex, index);
        const end = Math.max(this.lastSelectedSequenceIndex, index);
        this.checkedSequenceIndexes = [];
        for (let i = start; i <= end; i++) {
          this.checkedSequenceIndexes.push(i);
        }
      } else {
        this.lastSelectedSequenceIndex = index;
        const isChecked = this.checkedSequenceIndexes.includes(index);
        if (isChecked) {
          this.checkedSequenceIndexes = this.checkedSequenceIndexes.filter(
            (i) => i !== index
          );
        } else {
          this.checkedSequenceIndexes.push(index);
        }
      }
      for (const index of this.checkedSequenceIndexes) {
        console.log(index);
        const result = this.sequences[index].word;
        console.log("selectionné :" + result);
      }
    },

    toggleCheckedSequence(index) {
      const isChecked = this.checkedSequenceIndexes.includes(index);
      if (isChecked) {
        this.checkedSequenceIndexes = this.checkedSequenceIndexes.filter(
          (i) => i !== index
        );
      } else {
        this.checkedSequenceIndexes.push(index);
      }
    },
    selectAll() {
      if (this.checkedSequenceIndexes.length === this.sequences.length) {
        this.checkedSequenceIndexes = [];
      } else {
        this.checkedSequenceIndexes = this.sequences.map((_, index) => index);
      }
    },

    async enrichAndSend(lang) {
      this.enrichProgression = 0;
      this.isSending = true;
      let toBeEnrichedIndexes = this.checkedSequenceIndexes.slice();

      console.log(
        "checkedSequenceIndexes.length: " + toBeEnrichedIndexes.length
      );

      this.enrichProgression = 0;
      this.isSending = true;
      let enrichCount = 0;

      const total = toBeEnrichedIndexes.length;
      for (let index of toBeEnrichedIndexes) {
        const id = this.sequences[index].id;

        await axios
          .post(
            `${baseUrl}/api/v1/sequences/enrich-and-send-to-anki/${lang}?id=${id}`
          )
          .then((response) => {
            this.sequences[index].hasExplanations =
              response.data.hasExplanations;

            this.sequences[index].sentToAnkiTimes =
              response.data.sentToAnkiTimes;
          });
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isSending = false;
      const msg =
        total + " séquences ont été envoyées avec succès.";
      console.log(msg);
      new Notification(msg);
    },

    async import() {
      await axios
        .get(`${baseUrl}/api/v1/sequences`)
        .then((response) => {
          this.sequences = response.data;
          console.log("sequences importées");
        });
    },

    openFilePicker() {
      this.filePickerDialog = true;
    },
    openJsonPicker() {
      this.jsonPickerDialog = true;
    },
    onJsonSelected(event) {
      this.selectedJson = event.target.files[0];
    },
    async importJSON() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedJson);

        const response = await axios
          .post(
            `${baseUrl}/api/v1/sequences/import-json`,
            formData,
            {
              headers: {
                "Content-Type": "multipart/form-data",
              },
            }
          )
          .then((response) => {
            this.sequences = response.data;

            let msg = "Importation du fichier Json avec succès.";
            console.log(msg);
            new Notification(msg);
          });
      } catch (error) {
        console.error(error);

        let msg =
          "Une erreur est survenue lors de l'importation du fichier Json.";
        console.log(msg);
        new Notification(msg);
      }

      this.jsonPickerDialog = false;
    },
  },
};
</script>
<template>
  <div class="container mt-5">

    <!-- Importer un fichier JSON -->
    <div class="card mb-4">
      <div class="card-header">Sélectionner un fichier JSON</div>
      <div class="card-body">
        <div class="input-group">
          <input
            type="file"
            class="form-control"
            ref="fileInput"
            accept=".json"
            @change="onJsonSelected"
          />
          <button class="btn btn-primary ml-3" @click="importJSON">
            Importer
          </button>
        </div>
      </div>
    </div>

    <!-- Enrichir -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <b-spinner variant="primary" v-if="isSending"></b-spinner>
          <span class="m-1"
            >Envoyer vers Anki {{ this.enrichProgression }}%</span
          >
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-secondary mr-2" @click="selectAll()">
          Sélectionner tout
        </button>
        <button class="btn btn-info mr-2" @click="enrichAndSend('english')">
          Enrichir et envoyer en anglais
        </button>
        <button class="btn btn-info" @click="enrichAndSend('dutch')">
          Enrichir et envoyer en néérlandais
        </button>
      </div>
    </div>

    <!-- Liste des mots -->
    <div class="card">
      <div class="card-body">
        <div class="list-group">
          <div
            v-for="(sequence, index) in sequences"
            :key="sequence.id"
            :class="[
              'list-group-item',
              { active: checkedSequenceIndexes.includes(index) },
            ]"
            @mousedown="selectSequence(index, $event.shiftKey)"
            @keydown.space.prevent="toggleCheckedSequence(index)"
            tabindex="0"
          >
            <input
              type="checkbox"
              class="form-check-input"
              v-model="checkedSequenceIndexes"
              :value="index"
            />
            <span>{{ sequence.word }}</span>
            <span class="ml-2">{{ sequence.translatedWord }}</span>
            <span v-if="sequence.hasExplanations" class="badge badge-success"
              >Enriched</span
            >
            <span
              v-if="sequence.sentToAnkiTimes != 0"
              class="badge badge-success"
              >Sent to Anki</span
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style>
.badge-success {
  background-color: green !important;
  color: white;
}
</style>
