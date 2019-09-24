<template>
  <v-layout column>
    <v-flex xs12 mb-5>
      <h2>Редактирование цеха</h2>
    </v-flex>
    <v-flex xs12 v-if="shop === null || shop === undefined || shop === 'undefined'" class="text-xs-center">
      <v-progress-circular
          :size="110"
          :width="10"
          color="blue"
          indeterminate
        >Load...</v-progress-circular>
    </v-flex>
    <v-form v-else lazy-validation v-model="form.valid" ref="form">
      <v-flex xs12 mb-5>
        <v-select :items="companies" item-text="name" item-value="id" v-model="form.companyId" label="Компания" :rules="companyRules" required></v-select>
      </v-flex>
    <v-flex xs12 mb-3>
      <v-text-field
            v-model="form.name"
            :rules="nameRules"
            :counter="100"
            label="Наименование цеха"
            required
            class="ma-0 pa-0"
          ></v-text-field>
    </v-flex>
    <v-flex xs12>
      <v-btn class="primary" large @click="save" :disabled="!form.valid" :loading="loading" >
        <i class="fa fa-save fa-2x mr-2"></i>
        Сохранить
      </v-btn>
      <v-btn to="/shops" class="silver" large>
        <i class="fa fa-times fa-2x mr-2"></i>
        Отмена
      </v-btn>
    </v-flex>
    </v-form>
  </v-layout>
</template>

<script>
import { mapActions, mapGetters, mapState } from 'vuex'
export default {
  props: ['id'],
  data () {
    return {
      loading: false,
      form: {
        valid: false,
        name: '',
        companyId: ''
      },
      nameRules: [
        v => !!v || 'Name is required',
        v => v.length <= 100 || 'Name must be less than 100 characters'
      ],
      companyRules: [v => !!v || 'Company is required']
    }
  },
  mounted () {
    setTimeout(() => {
      if (this.shop !== null && this.shop !== undefined && this.shop !== 'undefined') {
        this.form.name = this.shop.name
        this.form.companyId = this.shop.companyId
      }
    }, 100)
  },
  computed: {
    ...mapGetters(['getShopById']),
    ...mapState({
      companies: state => state.company.companies
    }),
    shop () {
      return this.getShopById(this.id)
    }
  },
  methods: {
    ...mapActions(['editShop']),
    save (e) {
      this.loading = true
      this.$store.dispatch('clearAllMessages')
      e.preventDefault()
      if (this.$refs.form.validate()) {
        this.editShop({ id: this.id, name: this.form.name, companyId: this.form.companyId })
          .then(() => {
            this.$router.push('/shops')
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
