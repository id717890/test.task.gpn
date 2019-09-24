<template>
  <!-- <section @contextmenu.prevent="handler"> -->
  <section>
    <router-view name="main"></router-view>
    <modals-container/>
  </section>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  name: 'App',
  components: {
  },
  methods: {
    ...mapActions(['getAllCompanies', 'getAllShops', 'getAllFields', 'getAllWellTypes', 'getAllWells']),
    handler () {
    }
  },
  async created () {
    this.getAllCompanies()
    this.getAllShops()
    this.getAllFields()
    this.getAllWells()
    this.getAllWellTypes()
  }
}
</script>

<style lang="scss">
@import './assets/scss/_app';
</style>
