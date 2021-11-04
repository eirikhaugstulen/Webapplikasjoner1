import React from "react";
import {ArrowBothIcon, CalendarIcon, ClockIcon} from "@primer/octicons-react";
import {Button} from "reactstrap";

export const SelectAvgang = ({ avganger }) => {
    console.log(avganger);
    return (
        <div
            className={'mt-3'}
        >
            {avganger.map(avgang => (
                <div
                    className={'border p-4 d-flex justify-content-between'}
                    key={avgang.avgangNummer}
                >
                    <div>
                        <p><ArrowBothIcon size={16} /> {avgang.strekning.fraSted.stedsnavn} - {avgang.strekning.tilSted.stedsnavn}</p>
                        <p><CalendarIcon size={16} /> {avgang.dato}</p>
                    </div>
                    <div>
                        <p><ClockIcon size={16} /> {avgang.klokkeslett} </p>
                    </div>
                    <div>
                        <Button
                            outline
                            color={'primary'}
                        >
                            Velg
                        </Button>
                    </div>
                </div>
            ))}
        </div>
    )
}