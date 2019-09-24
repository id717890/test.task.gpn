<template>
  <v-layout column>
    <v-flex xs12 mb-5>
      <h2>Редактирование компании</h2>
    </v-flex>
    <v-flex xs12 v-if="company === null || company === undefined || company === 'undefined'" class="text-xs-center">
      <v-progress-circular
          :size="110"
          :width="10"
          color="blue"
          indeterminate
        >Load...</v-progress-circular>
    </v-flex>
    <v-form v-else lazy-validation v-model="form.valid" ref="form">
    <v-flex xs12 mb-3>
      <app-notify/>
      <v-text-field
            v-model="form.name"
            :rules="nameRules"
            :counter="100"
            label="Наименование компании"
            required
            class="ma-0 pa-0"
          ></v-text-field>
    </v-flex>
    <v-flex xs12 mb-3>
      <v-text-field
            v-model="form.shortName"
            :rules="nameRules"
            :counter="100"
            label="Краткое наименование компании"
            required
            class="ma-0 pa-0"
          ></v-text-field>
    </v-flex>
    <v-flex xs12>
      <v-btn class="primary" large @click="save" :disabled="!form.valid" :loading="loading" >
        <i class="fa fa-save fa-2x mr-2"></i>
        Сохранить
      </v-btn>
      <v-btn to="/companies" class="silver" large>
        <i class="fa fa-times fa-2x mr-2"></i>
        Отмена
      </v-btn>
    </v-flex>
    </v-form>
  </v-layout>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
export default {
  props: ['id'],
  data () {
    return {
      loading: false,
      form: {
        valid: false,
        name: '',
        shortName: ''
      },
      nameRules: [
        v => !!v || 'Name is required',
        v => v.length <= 100 || 'Name must be less than 100 characters'
      ]
    }
  },
  async mounted () {
    setTimeout(() => {
      if (this.company !== null && this.company !== undefined && this.company !== 'undefined') {
        this.form.name = this.company.name
        this.form.shortName = this.company.shortName
      }
    }, 100)
  },
  computed: {
    ...mapGetters(['getCompanyById']),
    company () {
      return this.getCompanyById(this.id)
    }
  },
  methods: {
    ...mapActions(['editCompany']),
    save (e) {
      this.loading = true
      this.$store.dispatch('clearAllMessages')
      e.preventDefault()
      if (this.$refs.form.validate()) {
        this.editCompany({ id: this.id, name: this.form.name, shortName: this.form.shortName })
          .then(() => {
            this.$router.push('/companies')
          })
          .catch(x => {
            this.loading = false
          })
      } else this.loading = false
    }
  }
}
</script>

<style>

</style>
