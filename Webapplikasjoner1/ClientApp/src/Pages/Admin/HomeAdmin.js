import React from "react";
import {Route, Switch, useRouteMatch} from "react-router-dom";

export const HomeAdmin = () => {
    const match = useRouteMatch();
    debugger;
    return (
        <>
            <Switch>
                <Route path={`${match.url}/test`} component={} />
            </Switch>
        </>
    );
};