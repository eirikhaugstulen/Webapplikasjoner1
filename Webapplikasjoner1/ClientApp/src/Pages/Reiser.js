import React, {useEffect, useState} from "react";
import {Badge, Button, Table} from "reactstrap";
import axios from "axios";
import {StatusBadge} from "../components/StatusBadge";
import history from "../history";

const fetchBilletter = async() => {
    const res = await axios('/Billett/hentalle');
    return res;
};

export const Reiser = () => {
    //usestate
    const [billetter, setBilletter] = useState([]);
    //useeffect res
    useEffect( () => {
        fetchBilletter()
            .then(res => convertInputBilletter(res.data))
    }, [])
    
    const convertInputBilletter = (inpBilletter) => {
        if (!inpBilletter) {
            return null;
        }
        
        const sortedBilletter = inpBilletter
            .filter(billett => billett.dato)
            .map(billett => {
                billett.dato = convertDate(billett.dato)
                billett.returDato = convertDate(billett.returDato)
                billett.status = billett.dato > new Date().setHours(1)
                return billett;
            })
            .sort((a,b) => (b.dato - a.dato))
        
        setBilletter(sortedBilletter);
    }
    
    const convertDate = (dateStr) => new Date(Date.parse(dateStr));
   
    return (
        <div className={'bg-white'}>
            <h2 className={'my-4'}>Dine reiser</h2>
            <Table className={'table table-hover border'}>
                <thead className={"thead-light"}>
                <tr>
                    <th>Status</th>
                    <th>Avreisedato</th>
                    <th>Navn</th>
                    <th>Strekning</th>
                    <th>Retur</th>
                    <th>Returdato</th>
                    <th>Pris</th>
                    <th>Kvittering</th>
                    
                </tr>
                </thead>
                <tbody>
                {billetter && billetter.map(b=>
                    <tr key={b.id}>
                        <StatusBadge active={b?.status} />
                        <td>
                            {b.dato?.toLocaleDateString()}
                        </td>
                        <td>
                            {b.fornavn} {b.etternavn}
                        </td>
                        <td>
                            {b.fraSted} - {b.tilSted}
                        </td>
                        <td>
                            {b.retur ? 'Ja' : 'Nei'}
                        </td>
                        <td>
                            {b.retur && b.returDato.toLocaleDateString()}
                        </td>
                        <td>
                            {b.pris},-
                        </td>
                        <td>
                            <Button
                                size={'sm'}
                                onClick={() => history.push(`/kvittering?id=${b.id}`)}
                            >
                                Se kvittering
                            </Button>
                        </td>
                    </tr>
                )}
                </tbody>
            </Table>
        </div>
    )
}