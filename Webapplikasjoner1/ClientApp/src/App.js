import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home';
import {Reiser} from "./Pages/Reiser";


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path={'/reiser'} component={Reiser} />
      </Layout>
    );
  }
}
