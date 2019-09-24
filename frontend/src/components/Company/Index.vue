<template>
<v-layout row wrap>
    <v-flex xs12 v-if="companies === null" class="text-xs-center">
      <v-progress-circular
          :size="110"
          :width="10"
          color="blue"
          indeterminate
        >Load...</v-progress-circular>
    </v-flex>
    <v-flex xs12 v-else>
      <v-card-title class="ma-0 pa-0">
        <h2><i class="fa fa-globe"></i> Компании</h2>
          <v-spacer></v-spacer>
          <v-text-field class="pa-0 pb-3"
            v-model="search"
            append-icon="fa fa-search"
            label="Поиск"
            single-line
            hide-details
          ></v-text-field>
        </v-card-title>
      <v-data-table
        :headers="headers"
        :items="companies"
        class="elevation-1"
        :search="search"
        hide-actions
      >
        <template v-slot:items="props">
          <td>{{ props.item.name }}</td>
          <td>{{ props.item.shortName }}</td>
          <td>
            <section>
              <v-btn class="ma-0 add-btn" flat title="Добавить" icon color="primary" to="/company/create"><i class="fa fa-plus"></i></v-btn>
              <v-btn class="ma-0" flat title="Удалить" icon color="error" @click="callConfirmDialog2(props.item)"><i class="fa fa-trash-alt"></i></v-btn>
              <v-btn class="ma-0" flat title="Редактировать" icon color="success" :to="'/company/edit/'+props.item.id"><i class="fa fa-pencil-alt"></i></v-btn>
            </section>
          </td>
        </template>
        <template v-slot:no-data>
          <td colspan="2"><h4 class="text-xs-center">Нет данных</h4></td>
          <td><v-btn class="ma-0" flat title="Добавить" icon color="primary" to="/company/create"><i class="fa fa-plus"></i></v-btn></td>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import ConfirmDialogModal from '../Dialog/ConfirmDialog'

const settings = {
  height: 'auto',
  maxWidth: 400,
  adaptive: true,
  transition: 'nice-modal-fade',
  clickToClose: false
}

export default {
  data () {
    return {
      removedCompany: null,
      search: null,
      headers: [
        { text: 'Наименование', value: 'name' },
        { text: 'Сокращение', value: 'shortName' },
        { text: '', value: '', width: '170', sortable: false }
      ]
    }
  },
  methods: {
    ...mapActions(['resetConfirmDialogResult', 'deleteCompany', 'getAllCompanies']),
    callConfirmDialog2 (item) {
      this.removedCompany = item
      this.$modal.show(ConfirmDialogModal, { question: 'Внимание будет произведено каскадное удаление! Удалить запись?' }, settings, {
        'closed': this.confirmDeleteDialog
      })
    },
    confirmDeleteDialog () {
      if (this.confirmDialogResult === true) {
        this.resetConfirmDialogResult()
        this.deleteCompany(this.removedCompany)
        this.removedCompany = null
      }
    }
  },
  async created () {
    this.getAllCompanies()
  },
  computed: {
    ...mapState({
      companies: state => state.company.companies,
      confirmDialogResult: state => state.dialog.confirmDialogResult
    })
  }
}
</script>

<style>

</style>
