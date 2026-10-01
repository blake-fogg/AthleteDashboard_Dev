<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getAthletes } from '../services/athleteservice'
import type { Athlete } from '../types/athlete'

const athletes = ref<Athlete[]>([])

onMounted(async () => {
  try {
    athletes.value = await getAthletes()
  } catch (error) {
    console.error('Failed to load athletes:', error)
  }
})
</script>

<template>
  <div>
    <h1>Athlete Dashboard</h1>
    <p>Your training dashboard is coming together.</p>

    <table v-if="athletes.length" border="1" style="margin-top: 1rem; border-collapse: collapse;">
      <thead>
        <tr>
          <th style="padding: 8px 12px; text-align: left;">First Name</th>
          <th style="padding: 8px 12px; text-align: left;">Last Name</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="athlete in athletes" :key="athlete.AthleteID">
          <td style="padding: 8px 12px;">{{ athlete.FirstName }}</td>
          <td style="padding: 8px 12px;">{{ athlete.LastName }}</td>
        </tr>
      </tbody>
    </table>

    <p v-else style="margin-top: 1rem;">No athletes found.</p>
  </div>
</template>