import React, {useEffect, useState} from "react";
//Display lokasjoner
import {Button, Table} from "reactstrap";
import {ReiseTableRow} from "../../components/ReiseTableRow";

const reiselokasjoner = [
    {
        id: 1,
        displayName: 'Oslo',
    },
    {
        id: 2,
        displayName: 'Kristiansand',
    },
    {
        id: 3,
        displayName: 'Stavanger',
    },
    {
        id: 4,
        displayName: 'Bergen',
    },
    {
        id: 5,
        displayName: 'Ålesund',
    },
    {
        id: 6,
        displayName: 'Trondheim',
    }
]

export const Lokasjoner = () => {
    return(
        <div>
            <Table className={'table border'}>
                <thead className={'thead-light'}>
                <tr>
                    <td><h4>Lokasjoner</h4></td>
                </tr>
                </thead>
                <tbody >
                {reiselokasjoner.map(lokasjon =>
                    <tr> <td key={lokasjon.id}>{lokasjon.displayName}</td>
                    
                    <td><Button className={'btn btn-danger'}>Slett</Button></td></tr>
                )}
                </tbody>
            </Table>
            <Button className={'btn btn-success'}>Legg til lokasjon</Button>
        </div>
    )
}
//Legge til
//Slette



