import React, {useEffect, useState} from "react";
import {useHistory, useLocation} from "react-router-dom";
import axios from "axios";
import {Button, Table} from "reactstrap";

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
    const id = useQuery().get('id');
    const history = useHistory();
    
    useEffect(() => {
        fetchEnkeltbillettData(id)
            .then(res => setBillett(res.data))
            .catch(e => console.log(e))
        
    }, [id])
    
    return ( 
        <div className={'bg-white'}>
            <h2>Takk for ditt kjøp!</h2>
            <Table className={'table border'}>
                <thead>
                    <tr>
                        <td><h4>Ordredetaljer</h4></td>
                        
                    </tr>
                </thead>
                <tbody className={'table-bordered'}>
                    <tr>
                        <td>Navn</td>
                        <td>Jo Are</td>
                    </tr>
                    <tr>
                        <td>Ordrenummer</td>
                        <td>1</td>
                    </tr>
                    <tr>
                        <td>Avreisedato</td>
                        <td>06.10.2021</td>
                    </tr>
                    <tr>
                        <td>Returdato</td>
                        <td>08.10.2021</td>
                    </tr>
                    <tr>
                        <td>Strekning</td>
                        <td>Oslo - Stavanger</td>
                    </tr>
                    <tr>
                        <td>Pris</td>
                        <td>400,-</td>
                    </tr>
                </tbody>
            </Table>
            <Button onClick={() => history.goBack()} color={'primary'}>Tilbake</Button>
            <br/><br/>
            
            {JSON.stringify(billett)}
        </div>
    )
} 