<template>
    <q-layout view="hHh Lpr lFf">
        <!-- Header -->
        <q-header elevated>
            <q-toolbar>
                <q-btn flat dense round icon="sym_o_menu" aria-label="Toggle navigation" @click="toggleLeftDrawer" />

                <q-toolbar-title>
                    Athlete Dashboard
                </q-toolbar-title>

                <q-btn flat round dense icon="sym_o_settings" aria-label="Settings" />
            </q-toolbar>
        </q-header>

        <!-- Navigation -->
        <q-drawer v-model="leftDrawerOpen" bordered>
            <q-list padding>
                <q-item clickable v-ripple to="/" exact>
                    <q-item-section avatar>
                        <q-icon name="sym_o_dashboard" />
                    </q-item-section>

                    <q-item-section>
                        Dashboard
                    </q-item-section>
                </q-item>

            </q-list>
        </q-drawer>

        <!-- Page Content -->
        <q-page-container>
            <router-view />
        </q-page-container>
    </q-layout>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const leftDrawerOpen = ref(false)

function toggleLeftDrawer() {
    leftDrawerOpen.value = !leftDrawerOpen.value
}
</script>