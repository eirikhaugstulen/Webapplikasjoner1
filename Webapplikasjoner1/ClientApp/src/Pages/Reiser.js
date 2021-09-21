import React from "react";
import {Table} from "reactstrap";


export const Reiser = () => {

    const reiseData = [
        {
            id: 1,
            strekning: 'Oslo-Kiel',
            antall: 3,
            pris: 600,
            dato: '16.09.21'
        },
        {
            id: 2,
            strekning: 'Sandefjord-Strømstad',
            antall: 2,
            pris: 400,
            dato: '25.08.21'
        },
        {
            id: 3,
            strekning: 'Kristiansand-Hirtshals',
            antall: 1,
            pris: 200,
            dato: '21.07.21'
        },
        {
            id: 4,
            strekning: 'Sandefjord-Strømstad',
            antall: 4,
            pris: 800,
            dato: '03.06.21'
        },
        {
            id: 5,
            strekning: 'Larvik-Hirtshals',
            antall: 4,
            pris: 800,
            dato: '05.02.21'
        },
    ];
    return (
        <div className={'bg-white shadow-sm'}>
            <h2 className={'my-4'}>Dine reiser</h2>
            <Table className={'table table- table-hover '}>
                <thead className={"thead-light"}>
                <tr>
                    <th>Bestilt</th>
                    <th>Pris</th>
                    <th>Strekning</th>
                    <th>Antall</th>
                </tr>
                </thead>
                <tbody>
                {reiseData.map((reise) => {
                    return (
                        <tr>
                            <td>
                                {reise.dato}
                            </td>
                            <td>
                                {reise.pris},-
                            </td>
                            <td>

                                {reise.strekning}
                            </td>
                            <td> 
                                {reise.antall}
                            </td>
                        </tr>
                    )
                })}
                </tbody>
            </Table>
        </div>
    )
}