import React from "react";
import {Route, Switch, useRouteMatch} from "react-router-dom";
import { Avganger } from './AvgangerAdmin';
import { Strekninger } from './StrekningerAdmin';
import { Lokasjoner } from './LokasjonerAdmin';

export const HomeAdmin = () => {
    const match = useRouteMatch();
    debugger;
    return (
        <>
            <Switch>
                <Route path={`${match.url}/avganger`} component={Avganger} />
                <Route path={`${match.url}/lokasjoner`} component={Lokasjoner} />
                <Route path={`${match.url}/strekninger`} component={Strekninger} />
            </Switch>
        </>
    );
};