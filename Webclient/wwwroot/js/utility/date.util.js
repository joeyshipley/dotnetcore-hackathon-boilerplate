BOS.UTIL.DATE = {

  formatSecondsToDDHHMMSS: function(total_seconds) {
    let remaining_seconds = BOS.UTIL.DATA.clone(total_seconds);

    const d = Math.floor(remaining_seconds / (3600 * 24));
    remaining_seconds -= d * 3600 * 24;

    let h = Math.floor(remaining_seconds / 3600);
    remaining_seconds -= h * 3600;

    let m = Math.floor(remaining_seconds / 60);
    remaining_seconds -= m * 60;

    if(h < 10) { h = `0${ h }`}
    if(m < 10) { m = `0${ m }`}
    if(remaining_seconds < 10) { remaining_seconds = `0${ remaining_seconds }`; }

    return `${ d }:${ h }:${ m }:${ remaining_seconds }`;
  }

};