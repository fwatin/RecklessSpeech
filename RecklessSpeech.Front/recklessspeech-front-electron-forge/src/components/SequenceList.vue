<script>
import axios from "axios";
const backendPort = process.env.NODE_ENV === "development" ? "47973" : "5001";
export default {
  data() {
    return {
      filePickerDialog: false,
      jsonPickerDialog: false,
      sequences: [],
      checkedSequenceIndexes: [],
      selectedFile: null,
      selectedJson: null,
      enrichProgression: 0,
      isEnriching: false,
      isSendingToAnki: false,
      sendToAnkiProgression: 0,
      lastSelectedSequenceIndex: null,
    };
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

    async enrichInEnglish() {
      this.enrichProgression = 0;
      this.isEnriching = true;

      console.log(
        "checkedSequenceIndexes.length: " + this.checkedSequenceIndexes.length
      );

      this.enrichProgression = 0;
      this.isEnriching = true;
      let enrichCount = 0;

      const total = this.checkedSequenceIndexes.length;
      for (let index of this.checkedSequenceIndexes) {
        const id = this.sequences[index].id;

        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/Dictionary/english?id=${id}`
          )
          .then((response) => {
            this.sequences[index].hasExplanations =
              response.data.hasExplanations;
          });
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isEnriching = false;
      const msg =
        total + " séquences ont été enrichies avec succès en anglais.";
      console.log(msg);
      new Notification(msg);
    },

    async enrichInDutch() {
      this.enrichProgression = 0;
      this.isEnriching = true;

      console.log(
        "checkedSequenceIndexes.length: " + this.checkedSequenceIndexes.length
      );

      this.enrichProgression = 0;
      this.isEnriching = true;
      let enrichCount = 0;

      const total = this.checkedSequenceIndexes.length;
      for (let index of this.checkedSequenceIndexes) {
        const id = this.sequences[index].id;

        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/Dictionary/dutch?id=${id}`
          )
          .then((response) => {
            this.sequences[index].hasExplanations =
              response.data.hasExplanations;
          });
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isEnriching = false;
      const msg =
        total + " séquences ont été enrichies avec succès en néérlandais.";
      console.log(msg);
      new Notification(msg);
    },

    async sendToAnki() {
      this.sendToAnkiProgression = 0;
      let sendToAnkiCount = 0;
      const total = this.checkedSequenceIndexes.length;
      for (const index of this.checkedSequenceIndexes) {
        const id = this.sequences[index].id;

        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/send-to-anki?id=${id}`
          )
          .then((response) => {
            this.sequences[index].sentToAnkiTimes =
              response.data.sentToAnkiTimes;
          })
          .catch(() => {
            new Notification(`${sequence.word} failed to be sent to Anki.`);
          });

        sendToAnkiCount++;
        this.sendToAnkiProgression = Math.round(
          (sendToAnkiCount * 100) / total
        );
      }
      new Notification(`${total} sequences sent to Anki.`);
    },

    openFilePicker() {
      this.filePickerDialog = true;
    },
    openJsonPicker() {
      this.jsonPickerDialog = true;
    },
    onFileSelected(event) {
      this.selectedFile = event.target.files[0];
    },
    onJsonSelected(event) {
      this.selectedJson = event.target.files[0];
    },
    async importZip() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedFile);

        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/import-zip`,
            formData,
            {
              headers: {
                "Content-Type": "multipart/form-data",
              },
            }
          )
          .then((response) => {
            this.sequences = response.data;
            console.log(
              this.sequences.length + " words set into the variable 'words'."
            );
            new Notification(this.sequences.length + " mots ont été importés.");
          });
      } catch (error) {
        console.error(error);
        new Notification(
          "Une erreur est survenue lors de l'importation du fichier zip."
        );
      }

      this.filePickerDialog = false;
    },
    async importJSON() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedJson);

        const response = await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/import-details`,
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
    <!-- Importer un fichier Zip -->
    <div class="card mb-4">
      <div class="card-header">Sélectionner un fichier Zip</div>
      <div class="card-body">
        <div class="input-group">
          <input
            type="file"
            class="form-control"
            ref="fileInput"
            accept=".zip"
            @change="onFileSelected"
          />
          <button class="btn btn-primary ml-3" @click="importZip">
            Importer
          </button>
        </div>
      </div>
    </div>

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
          <div
            v-if="isEnriching"
            class="spinner-border text-primary mr-2"
            role="status"
          ></div>
          Enrichir {{ this.enrichProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-secondary mr-2" @click="selectAll()">
          Sélectionner tout
        </button>
        <button class="btn btn-info mr-2" @click="enrichInEnglish()">
          Enrichir en anglais
        </button>
        <button class="btn btn-info" @click="enrichInDutch()">
          Enrichir en néérlandais
        </button>
      </div>
    </div>

    <!-- Envoyer à Anki -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <div
            v-if="isSendingToAnki"
            class="spinner-border text-primary mr-2"
            role="status"
          ></div>
          Envoyer {{ this.sendToAnkiProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-primary" @click="sendToAnki()">
          Envoyer vers Anki
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
