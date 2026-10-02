import 'quasar/src/css/index.sass'
import '@quasar/extras/material-symbols-outlined/material-symbols-outlined.css'
import './style.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { Quasar } from 'quasar'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(Quasar)

app.mount('#app')