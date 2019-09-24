<template>
  <v-layout column>
    <v-flex xs12 mb-5>
      <h2>Новая скважина</h2>
    </v-flex>
    <v-form lazy-validation v-model="form.valid" ref="form">
      <v-flex xs12 mb-2>
        <v-layout row wrap>
          <v-flex sm12 md4 pa-2>
            <v-select :items="companies" item-text="name" item-value="id" v-model="form.companyId" label="Компания" :rules="companyRules" required></v-select>
          </v-flex>
          <v-flex sm12 md4 pa-2>
            <v-select :items="shops" item-text="fullName" item-value="id" v-model="form.shopId" label="Цех" :rules="shopRules" required></v-select>
          </v-flex>
          <v-flex sm12 md4 pa-2>
            <v-select :items="fields" item-text="fullName" item-value="id" v-model="form.fieldId" label="Месторождение" :rules="fieldRules" required></v-select>
          </v-flex>
        </v-layout>
      </v-flex>
    <v-flex xs12 mb-3>
      <v-layout row wrap>
        <v-flex sm12 md6 pa-2 pt-4>
          <v-text-field v-model="form.name" :rules="nameRules" :counter="100" label="Номер скважины" required class="ma-0 pa-0"></v-text-field>
        </v-flex>
        <v-flex sm12 md6 pa-2>
          <v-select :items="wellTypes" item-text="name" item-value="id" v-model="form.wellTypeId" label="Тип скважины" :rules="wellTypeRules" required></v-select>
        </v-flex>
      </v-layout>
    </v-flex>
    <v-flex xs12 mb-3 pa-2>
      <v-layout row wrap>
        <v-flex xs12 md4 pa-2>
          <v-text-field v-model="form.altitude" :rules="altitudeRules" type="number" min="0" label="Альтитуда скважины" required class="ma-0 pa-0"></v-text-field>
        </v-flex>
        <v-flex xs12 md4 pa-2>
          <v-text-field v-model="form.zabI" :rules="zabIRules" type="number" min="0" label="Глубина забоя искусственного" required class="ma-0 pa-0"></v-text-field>
        </v-flex>
        <v-flex xs12 md4 pa-2>
          <v-text-field v-model="form.zabF" :rules="zabFRules" type="number" min="0" label="Глубина забоя фактического" required class="ma-0 pa-0"></v-text-field>
        </v-flex>
      </v-layout>
    </v-flex>
    <v-flex xs12>
      <v-btn class="primary" large @click="save" :disabled="!form.valid" :loading="loading" >
        <i class="fa fa-save fa-2x mr-2"></i>
        Сохранить
      </v-btn>
      <v-btn to="/wells" class="silver" large>
        <i class="fa fa-times fa-2x mr-2"></i>
        Отмена
      </v-btn>
    </v-flex>
    </v-form>
  </v-layout>
</template>

<script>
import { mapActions, mapState } from 'vuex'
export default {
  components: {
  },
  data () {
    return {
      loading: false,
      form: {
        valid: false,
        name: '',
        altitude: '',
        zabI: '',
        zabF: '',
        companyId: '',
        shopId: '',
        fieldId: '',
        wellTypeId: ''
      },
      nameRules: [
        v => !!v || 'Name is required',
        v => v.length <= 100 || 'Name must be less than 100 characters'
      ],
      companyRules: [v => !!v || 'Company is required'],
      fieldRules: [v => !!v || 'Field is required'],
      altitudeRules: [v => !!v || 'Altitude is required'],
      zabIRules: [v => !!v || 'Zaboy I is required'],
      zabFRules: [v => !!v || 'Zaboy F is required'],
      shopRules: [v => !!v || 'Shop is required'],
      wellTypeRules: [v => !!v || 'Well type is required']
    }
  },
  computed: {
    ...mapState({
      companies: state => state.company.companies,
      shops: state => state.shop.shops,
      fields: state => state.field.fields,
      wellTypes: state => state.well.wellTypes
    })
  },
  methods: {
    ...mapActions(['createWell']),
    save (e) {
      this.loading = true
      this.$store.dispatch('clearAllMessages')
      e.preventDefault()
      if (this.$refs.form.validate()) {
        this.createWell(this.form)
          .then(() => {
            this.$router.push('/wells')
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
