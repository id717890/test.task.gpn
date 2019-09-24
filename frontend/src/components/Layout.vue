<template>
  <v-app id="inspire">
    <v-toolbar
      app
      clipped-right
      dark
      class="blue-grey darken-3"
    >
      <i class="fab fa-2x fa-galactic-senate"></i>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down align-self-center" v-if="!drawer">
        <v-btn v-for="item in items" :key="item.title" flat :to="item.to"><i :class="'mr-2 ' + item.icon"></i> {{item.title}}</v-btn>
      </v-toolbar-items>
      <v-toolbar-side-icon @click.stop="drawer = !drawer">
        <fai icon="align-justify"></fai>
      </v-toolbar-side-icon>
    </v-toolbar>
    <v-navigation-drawer
      right
      v-model="drawer"
      dark
      class="blue-grey darken-3"
      app
    >
      <v-toolbar flat class="blue-grey darken-4">
        <v-toolbar-side-icon @click.stop="drawer = !drawer">
          <fai icon="align-justify"></fai>
      </v-toolbar-side-icon>
    </v-toolbar>
    <v-list dense class="pt-0">
      <v-list-tile
        v-for="item in items"
        :key="item.title"
        :to="item.to"
      >
        <v-list-tile-action>
          <i :class="item.icon"></i>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title>{{ item.title }}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
    </v-navigation-drawer>
    <v-content>
      <v-container fluid >
        <router-view name="routeradmin"></router-view>
      </v-container>
    </v-content>
    <div class="white--text blue-grey darken-3" style="z-index:10">
      <v-layout row wrap justify-center fill-height>
        <v-flex xs12 class="text-xs-center hidden-sm-and-down">
          <v-layout row nowrap justify-center align-center>
            <div class="py-0">&copy;2019 — <strong>All Rights Reserved.</strong></div>
          </v-layout>
        </v-flex>
        <v-flex xs12 class="text-xs-center hidden-md-and-up">
          <v-layout row nowrap justify-center align-center>
            <div class="py-0" style="border-top: 2px solid rgba(255,255,255, 0.1)">&copy;2019 — <strong>All Rights Reserved.</strong></div>
          </v-layout>
        </v-flex>
      </v-layout>
    </div>
  </v-app>
</template>

<script>
export default {
  name: 'App',
  components: {
  },
  data () {
    return {
      items: [
        { title: 'Скважины', icon: 'fa fa-gopuram fa-2x', to: '/wells' },
        { title: 'Месторождения', icon: 'far fa-map fa-2x', to: '/fields' },
        { title: 'Цеха', icon: 'fa fa-warehouse fa-2x', to: '/shops' },
        { title: 'Компании', icon: 'fa fa-globe fa-2x', to: '/companies' }
      ],
      drawer: true
    }
  },
  props: {
    source: String
  },
  methods: {
  }
}
</script>
