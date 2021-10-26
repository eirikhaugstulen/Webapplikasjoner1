import React from "react";
//Display lokasjoner
import {Table} from "reactstrap";

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
                <tr>
                    <th>Lokasjoner</th>
                    <th>Endre</th>
                    <th>Slett</th>
                </tr>
                <tr>
                    {}
                </tr>
            </Table>
        </div>
    )
}
// Legge til

// Slette 
