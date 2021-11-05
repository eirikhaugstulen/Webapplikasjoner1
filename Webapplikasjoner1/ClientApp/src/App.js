import React, {Component, useState} from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home';
import { Reiser } from "./Pages/Reiser";
import { Bestilling } from "./Pages/Bestilling/Bestilling";
import { Kvittering } from "./Pages/Kvittering";
import {AdminContainer} from "./Pages/Admin/AdminContainer";
import {AdminWithLogin} from "./Pages/Admin/AdminWithLogin";
import {KundeContext} from "./Context/KundeContext";


export const App = () => {
    const [KundeId, setKundeId] = useState('');
    return (
        <Layout>
            <KundeContext.Provider value={{ KundeId, setKundeId }}>
                <Route exact path='/' component={Home} />
                <Route path={'/reiser'} component={Reiser} />
                <Route path={'/bestill'} component={Bestilling} />
                <Route path={'/kvittering'} component={Kvittering} />
            </KundeContext.Provider>

            <Route path={'/admin'} render={() => (
                <AdminWithLogin>
                    <AdminContainer />
                </AdminWithLogin>
            )}  />
        </Layout>
    );
}
