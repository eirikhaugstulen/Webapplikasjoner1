import React, {useContext, useEffect, useState} from "react";
import {Table} from "reactstrap";
import axios from "axios";
import yacht from '../Media/yacht.svg';
import {ReiseTableRow} from "../components/ReiseTableRow";
import {KundeContext} from "../Context/KundeContext";

const fetchBilletter = async() => {
    const res = await axios('/Billett/hentalle');
    return res;
};

export const Reiser = () => {
    //usestate
    const [billetter, setBilletter] = useState([]);
    const { KundeId } = useContext(KundeContext);
    
    
    //useeffect res
    useEffect( () => {
        const convertInputBilletter = (inpBilletter) => {
            if (!inpBilletter) {
                return null;
            }
            
            const sortedBilletter = inpBilletter
                .filter(billett => billett.kundeId.kundeId === KundeId)
                .map(billett => {
                    billett.avgang.dato = convertDate(`${billett.avgang.dato} ${billett.avgang.klokkeslett}`)
                    billett.returDato = convertDate(billett.returDato)
                    billett.status = billett.avgang.dato > new Date().setHours(1)
                    return billett;
                })
                .sort((a,b) => (a.avgang.dato - b.avgang.dato))
            
            setBilletter(sortedBilletter);
        }
        
        fetchBilletter()
            .then(res => convertInputBilletter(res.data))
            .catch(e => console.log(e))
    }, [])
    
    
    const convertDate = (dateStr) => new Date(Date.parse(dateStr));
   
    return (
        <div className={'bg-white'}>
            <h2 className={'my-4'}>Dine reiser</h2>
            <Table className={'table table-hover table-responsive-lg border rounded-3'}>
                <thead className={"thead-light"}>
                <tr>
                    <th>Status</th>
                    <th>Avreisedato</th>
                    <th>Navn</th>
                    <th>Strekning</th>
                    <th>Retur</th>
                    <th>Pris</th>
                    <th>Kvittering</th>
                    
                </tr>
                </thead>
                <tbody>
                {billetter && billetter.map(b=> <ReiseTableRow b={b} key={b.id} />)}
                </tbody>
            </Table>
            <div
                className={'p-5 d-flex justify-content-center'}
            >
                <img 
                    src={yacht} 
                    alt={'Cruiseskip'}
                    className={'w-25'}
                />
            </div>
        </div>
    )
}