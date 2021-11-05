import React from "react";
import history from "../history";
import {StatusBadge} from "./StatusBadge";
import {Button} from "reactstrap";

export const ReiseTableRow = ({b}) => (
    <tr
        onClick={() => history.push(`/kvittering?id=${b.id}`)}
        style={{cursor: 'pointer'}}
    >
        <StatusBadge active={b?.status} />
        <td>
            {b.avgang.dato?.toLocaleDateString()}
        </td>
        <td>
            {b.kundeId?.fornavn} {b.kundeId?.etternavn}
        </td>
        <td>
            {b.avgang?.strekning?.fraSted?.stedsnavn} - {b.avgang?.strekning?.tilSted?.stedsnavn}
        </td>
        <td>
            {b.retur ? 'Ja' : 'Nei'}
        </td>
        <td>
            {b.totalPris},-
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
);