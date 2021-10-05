import React, {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";
import axios from "axios";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
}

const fetchEnkeltbillettData = async (id) => {
    return axios.get('/Billett/HentEn', {
        params: {
            id
        }
    }); 
}

export const Kvittering = () => {
    const [billett, setBillett] = useState();
    const id = useQuery().get('id')
    
    useEffect(() => {
        fetchEnkeltbillettData(id)
            .then(res => setBillett(res.data))
            .catch(e => console.log(e))
    }, [id])
    
    return ( 
        <>
            <h3>Takk for ditt kjøp!</h3>
            {JSON.stringify(billett)}
        </>
    )
} 