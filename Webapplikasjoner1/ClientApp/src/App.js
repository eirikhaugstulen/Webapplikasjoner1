import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home';
import { Reiser } from "./Pages/Reiser";
import { Bestilling } from "./Pages/Bestilling/Bestilling";
import { Kvittering } from "./Pages/Kvittering";
import { HomeAdmin } from "./Pages/Admin/HomeAdmin";


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path={'/admin'} component={HomeAdmin} />
            <Route path={'/reiser'} component={Reiser} />
            <Route path={'/bestill'} component={Bestilling} />
            <Route path={'/kvittering'} component={Kvittering} />
        </Layout>
    );
  }
}
