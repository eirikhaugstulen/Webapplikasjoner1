import React, {useState, useEffect, useCallback} from "react";
import {Route, Switch, useRouteMatch} from "react-router-dom";
import { Avganger } from './AvgangerAdmin';
import { Strekninger } from './StrekningerAdmin';
import { Lokasjoner } from './LokasjonerAdmin';
import axios from "axios";
import {Spinner} from "reactstrap";
import {AdminHome} from "./AdminHome";
import {LoggInnAdmin} from "./LoggInnAdmin";
import {checkUnauthorized} from "../../utils/checkUnauthorized";

export const AdminContainer = () => {
    const match = useRouteMatch();
    const [apiData, setApiData] = useState({
        lokasjoner: [], 
        strekninger: [],
        avganger: [],
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState();
    
    const fetchApiData = useCallback(() => {
        const apiKall = [];
        apiKall.push(axios.get('/Lokasjon/HentAlle'));
        apiKall.push(axios.get('/Strekning/HentAlle'));
        apiKall.push(axios.get('/Avganger/HentAlle'));
            
            Promise.all(apiKall).then(res => {
                setApiData({
                    lokasjoner: res[0].data,
                    strekninger: res[1].data,
                    avganger: res[2].data,
                });
                setLoading(false);
            })
            .catch(e => {
                checkUnauthorized(e);
                if (e?.response?.status !== 401) {
                    setError(e)
                }
                setLoading(false);
            });
    }, [])
    
    const refetch = useCallback(() => fetchApiData(), [fetchApiData]);
    
    useEffect(() => {
        fetchApiData();
    }, [fetchApiData])
    
    if (error) {
        return (
            <div className={'d-flex justify-content-center m-5'}>
                <div className={'text-center'}>
                    <p className={'text-danger'}>Det har skjedd en feil</p>
                    {JSON.stringify(error, null, 2)}
                </div>
            </div>
        );
    }
    
    if (loading) {
        return (
            <div className={'d-flex justify-content-center m-5'}>
                <div className={'text-center'}>
                    <Spinner
                        color="info"
                        type="grow"
                    >
                        Laster inn...
                    </Spinner>
                    <p>Laster inn</p>
                </div>
            </div>
        );
    }
    
    return (
        <>
            <Switch>
                <Route 
                    path={`${match.url}/lokasjoner`}
                    render={() => (
                        <Lokasjoner 
                            apiData={apiData}
                            refetch={refetch}
                        />
                    )} 
                />
                
                <Route 
                    path={`${match.url}/strekninger`} 
                    render={() => (
                        <Strekninger 
                            apiData={apiData}
                            refetch={refetch}
                        />
                    )} 
                />
                
                <Route 
                    path={`${match.url}/avgang`}
                    render={() => (
                        <Avganger
                            apiData={apiData}
                            refetch={refetch}
                        />
                    )}
                />

                <Route
                    path={`${match.url}/logginn`}
                    component={LoggInnAdmin}
                />
                
                <Route 
                    path={`${match.url}/`}
                    component={AdminHome}
                />
            </Switch>
        </>
    );
};