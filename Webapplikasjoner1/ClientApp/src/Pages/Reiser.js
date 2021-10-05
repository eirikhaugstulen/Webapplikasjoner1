import React, {useEffect, useState} from "react";
import {Badge, Table} from "reactstrap";
import axios from "axios";
import {StatusBadge} from "../components/StatusBadge";

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
                billett.status = billett.dato > new Date().setHours(1)
                return billett;
            })
            .sort((a,b) => (b.dato - a.dato))
        
        setBilletter(sortedBilletter);
    }
    
    const convertDate = (dateStr) => new Date(Date.parse(dateStr));
   
    return (
        <div className={'bg-white shadow-sm'}>
            <h2 className={'my-4'}>Dine reiser</h2>
            <Table className={'table table-hover '}>
                <thead className={"thead-light"}>
                <tr>
                    <th>Status</th>
                    <th>Bestilt</th>
                    <th>Navn</th>
                    <th>Strekning</th>
                    <th>Pris</th>
                    <th>Antall</th>
                    
                </tr>
                </thead>
                <tbody>
                {billetter && billetter.map(b=>
                        <tr>
                            <StatusBadge active={b?.status} />
                            <td>
                                {b.dato?.toLocaleDateString()}
                                {/*{b.dato}*/}
                            </td>
                            <td>
                                {b.fornavn} {b.etternavn}
                            </td>
                            <td>
                                {b.fraSted} - {b.tilSted}
                            </td>
                            <td>
                                {b.pris},-
                            </td>
                            <td>
                                {b.antall}
                            </td>
                        </tr>
                    )
                }
                </tbody>
            </Table>
        </div>
    )
}