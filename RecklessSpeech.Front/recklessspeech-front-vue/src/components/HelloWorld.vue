<template>
  <div>
    <v-btn @click="openFilePicker">Import items.csv</v-btn>
    <v-btn>Import lln_json_items_xxx.json</v-btn>
    <v-card>
      <v-card-title>Liste de séquences</v-card-title>
      <v-card-text>
        <v-list>
          <v-list-item v-for="(sequence, index) in sequences" :key="index">
            {{ sequence.word }}
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
    <v-dialog v-model="filePickerDialog" max-width="500">
      <v-card>
        <v-card-title>Sélectionner un fichier CSV</v-card-title>
        <v-card-text>
          <input type="file" ref="fileInput" accept=".csv" @change="onFileSelected">
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="importCSV">Importer</v-btn>
          <v-btn @click="filePickerDialog = false">Annuler</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
<script>
import axios from 'axios';

export default {
  data() {
    return {
      sequences: [],
      filePickerDialog: false,
      selectedFile: null
    }
  },
  mounted() {
    axios.get('https://localhost:47973/api/v1/sequences')
      .then(response => {
        this.sequences = response.data;
      })
      .catch(error => {
        console.error(error);
      });
  },
  methods: {
    openFilePicker() {
      this.filePickerDialog = true;
    },
    onFileSelected(event) {
      this.selectedFile = event.target.files[0];
    },
    async importCSV() {
      try {
        const formData = new FormData();
        formData.append('file', this.selectedFile);

        const response = await axios.post('https://localhost:47973/api/v1/sequences', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });

        console.log(response.data);
        // Mettre à jour la liste des séquences si besoin

        this.$notify({
          title: 'Succès',
          message: 'Le fichier CSV a été importé avec succès.',
          type: 'success'
        });
      } catch (error) {
        console.error(error);

        this.$notify({
          title: 'Erreur',
          message: 'Une erreur est survenue lors de l\'importation du fichier CSV.',
          type: 'error'
        });
      }

      this.filePickerDialog = false;
    }
  }
}
</script>