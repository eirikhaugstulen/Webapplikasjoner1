import React, {useEffect, useState} from "react";
import axios from "axios";
import {checkUnauthorized} from "../../utils/checkUnauthorized";
import {LoadingScreen} from "../../components/LoadingScreen";

const fetchLoginStatus = () => axios.get('/Admins/SjekkLoggetInn');

export const AdminWithLogin = ({ children }) => {
    const [loggetInn, setLoggetInn] = useState(false);
    const [loading, setLoading] = useState(true);
    
    useEffect(() => {
        fetchLoginStatus()
            .then(() => {
                setLoggetInn(true)
                setLoading(false);
            })
            .catch(e => {
                checkUnauthorized(e)
                setLoading(false);
            })
    }, [fetchLoginStatus, checkUnauthorized])
    
    if (loading) {
        return (
            <LoadingScreen />
        )
    }
    
    return (
        <>
            { children }
        </>
    )
}