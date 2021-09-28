import React, {useEffect, useState} from "react";
import {Table} from "reactstrap";
import axios from "axios";

const fetchBilletter = async() => {
    const res = await axios('/Billett/hentalle');
    return res;
};

export const Reiser = () => {
    //usestate
    const [billetter, setBilletter] = useState([]);
    //useeffect res
    useEffect( () => {
        fetchBilletter().then(res => setBilletter(res.data))
    }, [])
   
    return (
        <div className={'bg-white shadow-sm'}>
            <h2 className={'my-4'}>Dine reiser</h2>
            <Table className={'table table- table-hover '}>
                <thead className={"thead-light"}>
                <tr>
                    <th>Bestilt</th>
                    <th>Pris</th>
                    <th>Navn</th>
                    <th>Strekning</th>
                    <th>Antall</th>
                    
                </tr>
                </thead>
                <tbody>
                {billetter.map(b=>
                    <tr>
                        <td>
                            {b.dato}
                        </td>
                        <td>
                            {b.pris},-
                        </td>
                        <td>
                            {b.fornavn} {b.etternavn}
                        </td>
                        <td>
                            {b.strekning}
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