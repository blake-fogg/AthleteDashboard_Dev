<template>
  <div>
    <h1>Athlete Dashboard</h1>
    <q-btn @click="loadAthletes" label="Load Athletes" color="primary" />

    <q-table :rows="athletes" :columns="[
      { name: 'athleteID', label: 'Athlete ID', field: 'athleteID', align: 'left' },
      { name: 'firstName', label: 'First Name', field: 'firstName', align: 'left' },
      { name: 'lastName', label: 'Last Name', field: 'lastName', align: 'left' },
      { name: 'actions', label: 'Actions', field: 'actions', align: 'center', sortable: false }
    ]" row-key="athleteID" :pagination="{ rowsPerPage: 10 }" dark>
      <template #body-cell-actions="props">
        <q-td :props="props">
          <q-btn
            size="sm"
            color="primary"
            flat
            dense
            label="Log Row"
            @click="logRow(props.row)"
          />
        </q-td>
      </template>
    </q-table>

  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getAthletes } from '../services/athleteservice'
import { getStravaAthlete } from '../services/stravaservice'
import type { Athlete } from '../types/athlete'

const athletes = ref<Athlete[]>([])

onMounted(async () => {
  try {
    await loadAthletes()
  } catch (error) {
    console.error('Failed to load athletes:', error)
  }
})

const loadAthletes = async () => {
  try {
    athletes.value = await getAthletes()
    console.log('Athletes loaded:', athletes.value)
  } catch (error) {
    console.error('Failed to load athletes:', error)
  }
}

const logRow = async (row: Athlete) => {
  console.log('Row data:', row)

  try {
    const stravaAthlete = await getStravaAthlete(row.athleteID)
    console.log('Strava athlete:', stravaAthlete)
  } catch (error) {
    console.error('Strava athlete request failed:', error)
  }
}
</script>