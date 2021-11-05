import React, {useMemo} from "react";
import {ArrowBothIcon, CalendarIcon, ClockIcon} from "@primer/octicons-react";
import {Button} from "reactstrap";

export const SelectAvgang = (
    {
        avganger,
        avgangssted,
        ankomststed,
        dato,
        klokkeslett,
        selectAvgang,
        setStep,
        skipStep,
    }
) => {
    
    const handleSelectAvgang = (id) => {
        selectAvgang(id);
        setStep(prevState => prevState + skipStep);
    }
    
    let outputAvganger = useMemo(() => {
        let output = [...avganger];
        
        if (avgangssted) {
            output = output?.filter(avgang => avgang?.strekning?.fraSted?.stedsNummer === avgangssted)
        }
        if (ankomststed) {
            output = output?.filter(avgang => avgang?.strekning?.tilSted?.stedsNummer === ankomststed)
        }
        if (dato) {
            output = output?.filter(avgang => {
                const avgangDato = new Date(`${avgang.dato} ${avgang.klokkeslett || '00:00'}`)
                const fraDatoObject = new Date(`${dato} ${klokkeslett || '00:00'}`)
                return fraDatoObject < avgangDato;
            })
        }
        
        output = output?.sort((a, b) => new Date(`${a?.dato} ${a?.klokkeslett}`) - new Date(`${b?.dato} ${b?.klokkeslett}`))
        
        return output;
    }, [avganger, avgangssted, ankomststed])


    if (!outputAvganger?.length) {
        return (
            <p>Ingen avganger tilgjengelig</p>
        )
    }

    return (
        <div
            className={'mt-3'}
        >
            {outputAvganger?.map(avgang => (
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
                            onClick={() => handleSelectAvgang(avgang)}
                        >
                            Velg
                        </Button>
                    </div>
                </div>
            ))}
        </div>
    )
}