import React from "react";
import {BackButton} from "../../components/AdminHome/BackButton";
import {Row} from "reactstrap";
// Tabell med aktive strekninger (fra databasen)

// Kolonne fralokasjon og tilLokasjon

// Endre
// Slett
// Legg til

export const Strekninger = ({ apiData, refetch }) => {
return(
    <div>
        <Row className={'p-3'}>
            <BackButton />
        </Row>
    </div>
)
} 