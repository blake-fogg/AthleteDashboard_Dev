<template>
  <div>
    <h1>Athlete Dashboard</h1>

    <q-table
      :rows="athletes"
      :columns="[
        { name: 'athleteID', label: 'Athlete ID', field: 'athleteID', align: 'left' },
        { name: 'firstName', label: 'First Name', field: 'firstName', align: 'left' },
        { name: 'lastName', label: 'Last Name', field: 'lastName', align: 'left' }
      ]"
      row-key="athleteID"
      :pagination="{ rowsPerPage: 10 }"
      dark
    />

  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getAthletes } from '../services/athleteservice'
import type { Athlete } from '../types/athlete'

const athletes = ref<Athlete[]>([])

onMounted(async () => {
  try {
    athletes.value = await getAthletes()
    console.log('Athletes loaded:', athletes.value)
  } catch (error) {
    console.error('Failed to load athletes:', error)
  }
})
</script>