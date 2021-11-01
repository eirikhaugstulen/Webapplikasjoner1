import React from "react";
import {Button, Row, Table} from "reactstrap";
import {ArrowLeftIcon} from "@primer/octicons-react";
import history from "../../history";
import {BackButton} from "../../components/AdminHome/BackButton";
// Se ALLE avganger

// Filtrere fraLokasjon, tilLokasjon og Dato'

const avganger = [
    {
        id: 1,
        fraLokasjon: 'Oslo',
        tilLokasjon: 'Bergen',
        dato: '01.12.21'
    },
    {
        id: 2,
        fraLokasjon: 'Trondheim',
        tilLokasjon: 'Stavanger',
        dato: '04.11.21'
    },
    {
        id: 3,
        fraLokasjon: 'Oslo',
        tilLokasjon: 'Bergen',
        dato: '02.11.21'
    },
    {
        id: 4,
        fraLokasjon: 'Ålesund',
        tilLokasjon: 'Trondheim',
        dato: '22.10.21'
    },
    
]

export const Avganger = () => {
    return(
        <div>
            <Row className={'p-3'}>
                <BackButton />
            </Row>
            <Table className={'table border'}>
                <thead className={'thead-light'}>
                <tr>
                    <td><h4>Avganger</h4></td>
                    <td />
                    <td className={'text-right'}><Button>Filtrer</Button></td>
                </tr>
                <tr className={'table-bordered font-weight-bold'}>
                    <td>Avreisested</td>
                    <td>Ankomststed</td>
                    <td>Dato</td>
                </tr>
                </thead>
                <tbody className={'table-bordered'}>
                {avganger.map(avgang =>
                <tr>
                    <td key={avgang.id}>{avgang.fraLokasjon}</td>
                    <td>{avgang.tilLokasjon}</td>
                    <td>{avgang.dato}</td>
                </tr>
                )}
                </tbody>
            </Table>
            
        </div>
    )
} 