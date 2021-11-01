import React, {useEffect, useState} from "react";
//Display lokasjoner
import {Button, Col, Row, Table} from "reactstrap";
import {BackButton} from "../../components/AdminHome/BackButton";
import {LeggTilLokasjon} from "../../components/AdminHome/LeggTil/LeggTilLokasjon";

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
            <Row className={'p-3'}>
                <BackButton />
            </Row>
            <Row>
                <Col
                    md={6}
                    sm={12}
                >
                    <Table className={'table border'}>
                        <thead className={'thead-light'}>
                        <tr>
                            <td><h4>Lokasjoner</h4></td>
                        </tr>
                        </thead>
                        <tbody>
                            {reiselokasjoner.map(lokasjon => (
                                <tr>
                                    <td 
                                        key={lokasjon.id}
                                    >
                                        {lokasjon.displayName}
                                    </td>
                                    <td>
                                        <Button 
                                            className={'btn btn-danger'}
                                        >
                                            Slett
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            <Row className={'p-3'}>
                <LeggTilLokasjon />
            </Row>
        </div>
    )
}
//Legge til
//Slette



