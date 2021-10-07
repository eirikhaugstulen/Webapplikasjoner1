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
            {b.dato?.toLocaleDateString()}
        </td>
        <td>
            {b.fornavn} {b.etternavn}
        </td>
        <td>
            {b.fraSted} - {b.tilSted}
        </td>
        <td>
            {b.retur ? 'Ja' : 'Nei'}
        </td>
        <td>
            {b.retur && b.returDato.toLocaleDateString()}
        </td>
        <td>
            {b.pris},-
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