import React, {useRef, useState, useEffect, useCallback} from "react";
import {Route, Switch, useRouteMatch} from "react-router-dom";
import { Avganger } from './AvgangerAdmin';
import { Strekninger } from './StrekningerAdmin';
import { Lokasjoner } from './LokasjonerAdmin';
import axios from "axios";
import {Spinner} from "reactstrap";
import {AdminHome} from "./AdminHome";


export const AdminContainer = () => {
    const match = useRouteMatch();
    const [apiData, setApiData] = useState({
        lokasjoner: {}, 
        strekninger: {},
        avganger: {},
    });
    const [loading, setLoading] = useState(true);
    const error = useRef();
    
    useEffect(() => {
        fetchApiData();
    }, [])
    
    const fetchApiData = () => {
        axios.get('/Billett/HentEn', {
            params: {
                id: '1',
            }
        })
            .then(res => {
                formaterApiData(res.data)
                setLoading(false);
            })
            .catch(e => error.current = e);
    }
    
    const refetch = useCallback(() => fetchApiData(), [fetchApiData]);
    
    const formaterApiData = (resApiData) => {
        setApiData({
            lokasjoner : resApiData?.[0],
            strekninger : resApiData?.[1],
            avganger : resApiData?.[2],
        });
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
                    path={`${match.url}/avganger`}
                    render={() => (
                        <Avganger
                            apiData={apiData}
                            refetch={refetch}
                        />
                    )}
                />
                
                <Route 
                    path={`${match.url}/`}
                    component={AdminHome}
                />
            </Switch>
        </>
    );
};