import React from "react";
import {NavigationButton} from "../../components/Admin/NavigationButton";
import {Col} from "reactstrap";

export const AdminHome = () => (
    <div>
        <div className="row">
            <div className={'col-sm mt-2'}>
                <NavigationButton 
                    tittel={'Reiselokasjoner'}
                    tekst={'Legg til eller endre lokasjoner som ferjer kan reise fra og til. Disse lokasjonene er det som bygger opp reisestrekningene.'}
                    page={'lokasjoner'}
                />
            </div>
            <div className={'col-sm mt-2'}>
                <NavigationButton 
                    tittel={'Strekninger'}
                    tekst={'Legg til eller endre strekninger som ferjer reiser mellom. Disse strekningene blir maler for å sette opp flere avganger.'}
                    page={'strekninger'}
                />
            </div>
            <div className={'col-sm mt-2'}>
                <NavigationButton
                    tittel={'Avganger'}
                    tekst={'Legg til eller endre avganger. Avganger er strekninger som man kan sette opp til å gå flere ganger.'}
                    page={'avganger'}
                />
            </div>
        </div>
        <div className="row mt-4">
            <Col lg>
                <NavigationButton 
                    tittel={'Readme'}
                    tekst={'Her kan vi legge inn litt utfyllende info om admin-sidene, hvordan man endrer ulike ting og hvorfor vi har gjort som vi gjør.'}
                />
            </Col>
        </div>
    </div>
);