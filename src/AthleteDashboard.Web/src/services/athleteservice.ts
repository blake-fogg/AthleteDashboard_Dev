import type { Athlete } from '../types/athlete'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? ''

export async function getAthletes(): Promise<Athlete[]> {
  const response = await fetch(`${API_BASE_URL}/api/Athlete/getAll`)
  if (!response.ok) {
    throw new Error(`Failed to fetch athletes: ${response.status} ${response.statusText}`)
  }

  return response.json()
}