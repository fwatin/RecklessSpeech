<template>
   <v-btn>Import items.csv</v-btn>
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
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      sequences: []
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
  }
}
</script>

