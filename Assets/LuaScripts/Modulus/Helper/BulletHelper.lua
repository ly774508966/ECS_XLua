BulletHelper = { }

function BulletHelper:getData(cfgId)
	local bulletConf = ConfigHelper:getConfigByKey('BulletConfig',cfgId)
	if not bulletConf then 
		return nil 
	end 
    local bdata = LuaExtend:getBulletData()
    --此处赋值cfg信息
    bdata.luaType = bulletConf.type 
    bdata.cfgId = id 
    bdata.speed = bulletConf.speed
    local effConf = ConfigHelper:getConfigByKey('EffectConfig',bulletConf.startEff)
    if effConf then 
       bdata.effPath = effConf.path       
    end     
    local expEffConf = ConfigHelper:getConfigByKey('EffectConfig',bulletConf.expEff)
    if expEffConf then 
    	bdata.expPath = expEffConf.path
    end 
    return bdata
end 