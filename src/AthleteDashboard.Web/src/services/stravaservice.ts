export interface StravaAthlete {
  id?: number
  username?: string
  firstname?: string
  lastname?: string
  city?: string
  state?: string
  country?: string
  profile_medium?: string
  profile?: string
  [key: string]: unknown
}

const API_BASE_URL = 'http://localhost:5118'

export async function getStravaAthlete(athleteId: number): Promise<StravaAthlete> {
  const response = await fetch(`${API_BASE_URL}/api/Strava/athlete?athleteId=${athleteId}`)

  if (!response.ok) {
    throw new Error(`Failed to fetch Strava athlete: ${response.status} ${response.statusText}`)
  }

  return response.json()
}